# MicroservicesWithDesignPatterns
Saga Design Pattern, Event Sourcing Pattern, Retry Pattern, Circuit Breaker Pattern, API Composition Pattern

Giriş => 
Microservice mimarilerinde ideal olarak her bir servisin kendine ait bir veritabanı olması gerekmektedir. İşte bu birden fazla servise dağıtılmış veritabanı sistemi distributed transaction olarak nitelendirilmektedir. Her servisin kendine ait bir veritabanı olması dolayısıyla bu veritabanların arasında yapılan işe/hizmete göre bütünsel bir veri tutarlılığının sağlanmasını gerektirmektedir.
Farz edelim ki bir e-ticaret yazılımı geliştiriyoruz. Bu yazılımın sipariş alma işlemlerini ‘Order’, siparişten sonra stok işlemlerini ‘Stock’ ve son olarak da ödeme işlemlerini ‘Payment’ servislerinde yaptığımızı düşünelim. Tabi ki de tüm bu servisin kendilerine ait veritabanları olduğunu düşünelim ve senaryotik açıdan akışın şu şekilde olduğunu düşünürsek ;

`Kullanıcı herhangi bir ürüne dair sipariş verdiğinde ‘Order’ servisi bu işlemi gerçekleştirecek ve kendi veritabanına ekleyecektir. ‘Payment’ servisi ise ödemeyi sağlayacak ve ödeme başarılıysa ‘Stock’ servisinde ki ilgili ürüne karşılık gelen stok miktarını düşürecektir.`

Şimdi bu işlem sırasında, ‘Order’ servisi gelen siparişi oluşturduktan sonra ‘Payment’ servisi o siparişe dair ödemeyi başarılı gerçekleştirirse eğer ‘Stock’ servisinde siparişteki ilgili ürüne dair stok adedinin düşürülmesi gerekmektedir. Aksi taktirde bir tutarsızlık meydana gelecektir. Düşünsenize, sipariş verilip ödeme yapıldıktan sonra hala stok miktarı aynı kalmaktadır. Bu durumun yazılımdaki verisel istatistiklere ve o yazılımı kullanan işletmeye verdiği zararı düşünün!

İşte  birden fazla serviste birden fazla veritabanıyla çalışılması(distributed transaction) durumlarında veri tutarlılığının sağlanmasından kastedilen budur.Distributed transaction senaryolarında bu bahsedilen veri tutarlılığının sağlanabilmesi için önerilen çözümlerden biri olan Saga patterndir.
### Transaction, Distributed Transaction ve Compensable Transactions Nedir?
* #### Transaction
Transaction, veritabanı üzerinde yapılan tüm işlemlere verilen genel isimdir.
* #### Distributed Transaction
Distributed Transactionlar birden fazla farklı veritabanının bir bütün olarak çalıştığı durumu ifade eder. Bu terim dağıtılmış veritabanı sistemleri için kullanılmaktadır.  Genellikle microservice gibi yaklaşımlarda her bir servisin kendi veritabanını taşıması distributed transaction olarak nitelendirilir.
* #### Compensable Transaction
Compensable Transaction ise, bir transaction’ın yapmış olduğu işlemin tersini almaktır. Yani commit edilen bir işlemin geriye dönüklüğü bu saatten sonra ancak telafi ile mümkündür. İşte bu geri dönük telafi işlemine compensable transactions denmektedir. Misal olarak; ‘A’ servisi yapmış olduğu bir işlemi ‘B’ servisindeki duruma göre iptal etmek zorundaysa ‘A’ servisi bunu ancak yapılan işin tam tersini alarak gerçekleştirebilmektedir. Örneğin, 100 değerine +10 eklendiyse, bu işlemin iptali -10 eklenmesidir. Bu kavramın teknik olarak Saga pattern’ın da çok kullanılır.
ACID Prensipler

ACID, değişikliklerin bir veri tabanına nasıl uygulanacağını yöneten 4 adet prensip sunar. Bunlar, Atomicity, Consistency, Isolation ve Durability prensipleridir. Bir kaç cümle ile açıklamak gerekirse;

Atomicity: En kısa ifadesiyle ya hep, ya hiç. Arda arda çalışan transaction’lar için iki olası senaryo vardır. Ya tüm transaction’lar başarılı olmalı ya da bir tanesi bile başarısız olursa tümünün iptal edilmesi durumudur.

Consistency: Veri tabanındaki datalarımızın tutarlı olması gerekir. Eğer bir transaction geçersiz bir veri üreterek sonuçlanmışsa, veri tabanı veriyi en son güncel olan haline geri alır. Yani bir transaction, veri tabanını ancak bir geçerli durumdan bir diğer geçerli duruma güncelleyebilir.

Isolation: Transaction’ların güvenli ve bağımsız bir şekilde işletilmesi prensibidir. Bu prensip sıralamayla ilgilenmez.Bir transaction, henüz tamamlanmamış bir başka transaction’ın verisini okuyamaz.

Durability: Commit edilerek tamamlanmış transaction’ların verisinin kararlı, dayanıklı ve sürekliliği garanti edilmiş bir ortamda (sabit disk gibi) saklanmasıdır. Donanım arızası gibi beklenmedik durumlarda transaction log ve alınan backup’lar da prensibe bağlılık adına önem arz etmektedir.
Saga Pattern Nedir?
Saga Pattern ile oluşturulan sistemlerde gelen istek ile daha sonraki her adım, bir önceki adımın başarılı şekilde tamamlanması sonrasında tetiklenir. Herhangi bir failover durumunda işlemi geri alma veya bir düzeltme aksiyonu almayı sağlayan pattern’dir.
SAGA TÜRLERİ
Orchestration-Based Saga
Bu yaklaşımda tüm işlemleri yöneten bir Saga orchestrator’u vardır. Bu orchestrator subscribe olan tüm consumer’lara ne zaman ne yapacağını ileten bir consumer’dır. Örnek bir e-ticaret senaryosu üzerinden Orchestration yaklaşımını şematize etmek gerekirse;

* Order service’imiz pending durumunda siparişi oluşturur ve Orchestrator’a ORDER_CREATED bilgisini gönderir ve Orchestrator’umuz sipariş oluşturma transaction’ınını başlatır.
* Orchestrator EXECUTE_PAYMENT komutunu Payment service’ine gönderir ve Payment geriye ödeme alındığı ile ilgili bilgiyi döndürür.(Başarılı olduğu senaryolar için..)
* Orchestrator UPDATE_STOCK komutunu Stock service’ine gönderir ve stock service’i geriye ilgili ürünlerin stok bilgisinin güncellendiği ile ilgili bilgiyi döndürür.(Başarılı olduğu senaryolar için..)
* Orchestrator ORDER_DELIVER komutunu Shipment service’ine gönderir ve Shipment geriye siparişin kargolandı bilgisini döndürür.(Başarılı olduğu senaryolar için..)

> Her durum için saga üzerinde state tutmak hangi adımda süreci yanlış yönettiğinizi görmeyi kolaylaştıracaktır.

Yukarıda açıklamaya çalıştığım durumlarda, Orchestrator’ümüz bir siparişin oluşturulması için gerekli akışın ne olduğunu bilir. Olası bir failover durumunda, işlemleri geri almak için subscribe olmuş tüm service’lere geri alma işlemini koordine etmekten sorumludur. Saga Orchestrator yaklaşımı ile State Pattern’i kullanmak iyi bir çözümdür. State Pattern’in uygulanması kolay ve test edilebilirliği basitleştirdiği için yerinde bir tercih olacaktır.
Akışımızda oluşacak bir hata durumunda failover sürecinden bahsetmek gerekirse;

1- Stock service’i Orchestrator’a OUT_OF_STOCK bilgisini gönderir.

2- Orchestrator işlemin başarısız olduğu bilgisini alıp rollback işlemini başlatır.

> Saga Orchestrator abone olan tüm service’leri çağırırabilir ancak, subscribe olan servisler orchestrator’ü çağıramaz. Çağırmamalıdır!!


**Avantajları ve Dezavantajları**

* Distributed transaction yönetimini merkezileştirir.
* Implemente etmek ve test etmek kolaydır.
* Rollback yönetimi basittir.
* Yeni step’ler eklendiğinde işlem karmaşıklığı cherography yaklaşımına göre daha az kompleksleşir.
* Birbirini bekleyen işlemleri bu yaklaşım ile yönetebilirsiniz.
* Fazladan bir service’i yönetmeniz gerektiği için sisteminizdeki infrastructure karmaşıklığı artacaktır.

https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/saga/saga#:~:text=The%20Saga%20design%20pattern%20is,trigger%20the%20next%20transaction%20step.
