namespace JwtTokenBaseAuthentication.DTO
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

        public int Quntity { get; set; }
    }

    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

      
    }


    public class updateProductDto   
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

        public int Quntity { get; set; }
    }


}
