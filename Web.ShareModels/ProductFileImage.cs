namespace Web.ShareModels
{
    public class ProductFileImage
    {
        public int ProductId {get; set;}

        public int FileImageId { get; set; }

        public FileImage FileImage { get; set; }
    }
}
