namespace EcomMVC.Models
{
    public class PaymentDetails
    {
        public string Id { get; set; }
        public string TransactionId { get; set;}
        public decimal Tax{ get; set; }        
        public decimal Total{ get; set; }
        public Guid CartId { get; set; }

        public int UserId { get; set; }

        public decimal FinalTotal   { get; set; }
    }
}