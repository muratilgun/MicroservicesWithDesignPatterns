# MicroservicesWithDesignPatterns
Saga Design Pattern, Event Sourcing Pattern, Retry Pattern, Circuit Breaker Pattern, API Composition Pattern

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
