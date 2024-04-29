namespace api
{
    public class Payment
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }

        public string Status { get; set; }
    }
}
