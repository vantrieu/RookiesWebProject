namespace Web.ShareModels
{
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Total { get; set; }

        public virtual Product Product { get; set; }
    }
}
