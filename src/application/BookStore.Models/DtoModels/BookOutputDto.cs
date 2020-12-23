using System;

namespace BookStore.Models.DtoModels
{
    public class BookOutputDto: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsDisabled { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
