using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController: ControllerBase
    {
        private static List<Book> BookList=new List<Book>()
        {
            new Book{
                Id=1,
                Title="Lean Startup",
                GenreId=1, //Personal Growth
                PageCount=200,
                PublishDate= new DateTime(2001,06,12),
                

            },
            new Book{
                Id=2,
                Title="Herland",
                GenreId=2, //Science Fiction
                PageCount=250,
                PublishDate= new DateTime(2010,05,23),
                

            },
            new Book{
                Id=3,
                Title="Dune",
                GenreId=3, //Science Fiction
                PageCount=540,
                PublishDate= new DateTime(2010,12,21),
                
            }                                
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList=BookList.OrderBy(x=>x.Id).ToList<Book>();
            return bookList;
        }
        // Route
        [HttpGet("{id}")]
        public Book GetByID(int id)
        {
            //LINQ
            var book=BookList.Where(book=>book.Id ==id).SingleOrDefault();
            return book;
        }     
        // [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book=BookList.Where(book=>book.Id ==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // } 

        // Post İşlemi
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook){
            var book = BookList.SingleOrDefault(x=>x.Title== newBook.Title);
            // if(book!=null) 
            // or
            if(book is not null )
                return BadRequest();
            // Eğer databasede var olan bir kitap varsa hata fırlatırız eğer yoksa kitap listesine ekliyoruz.
            BookList.Add(newBook);
                return Ok();
        } 
        // PUT İşlemi    
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book= BookList.SingleOrDefault(x=>x.Id== id);
            if(book is null)
                return BadRequest();
                
            book.GenreId=updatedBook.GenreId!=default ? updatedBook.GenreId : book.GenreId; //verisi doldurulmuşsa değiştir, değiştirilmemişse varsayılanı kullan
            book.PageCount=updatedBook.PageCount!=default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate=updatedBook.PublishDate!=default ? updatedBook.PublishDate : book.PublishDate;
            book.Title=updatedBook.Title!=default ? updatedBook.Title : book.Title;

            return Ok();
        }  
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            var book = BookList.SingleOrDefault(x=>x.Id== id);
            if(book is null )
                return BadRequest();
            // Eğer databasede var olan bir kitap varsa hata fırlatırız eğer yoksa kitap listesine ekliyoruz.
            BookList.Remove(book);
                return Ok();
        }              
    }
}