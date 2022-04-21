using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Automatonymous;

namespace SagaStateMachineWorkerService.Model
{
    public class OrderStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string BuyerId { get; set; }
        public int OrderId { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public override string ToString()
        {
            var properties = GetType().GetProperties();
            var sb = new StringBuilder();
            properties.ToList().ForEach(p =>
            {
                var value = p.GetValue(this, null);
                sb.AppendLine($"{p.Name} : {value}");
            });
            sb.AppendLine("-----------------");
            return sb.ToString();
        }
    }
}