using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserServiceHub.Models;

namespace UserServiceHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private DatabaseContext _DatabaseContext;

        public BooksController(DatabaseContext db)
        {
            _DatabaseContext = db;
        }

        [HttpGet]
        [Route("get_booklist")]
        public object get_BookDetails()
        {
            listBookModel obj = new listBookModel();
            try
            {
                obj.booklist = _DatabaseContext.Books.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return obj;
        }

        [HttpGet]
        [Route("get_booklist/{id:int}")]
        public object get_BookDetails(int id)
        {
            listBookModel obj = new listBookModel();
            try
            {
                obj.booklist = _DatabaseContext.Books.Where(t=>t.BookId==id).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return obj;
        }

        [HttpPost]
        [Route("savebookdetails")]
        public listBookModel save_Book_details([FromBody] listBookModel data)
        {
            try
            {
                if (data.BookId == 0)
                {
                    var duplicate = _DatabaseContext.Books.Where(t => t.BookTitle == data.BookTitle).ToList();

                    if (duplicate.Count > 0)
                    {
                        data.duplicate = true;
                    }
                    else
                    {
                        // Insert book Details
                        BooksModel obj = new BooksModel();
                        obj.BookTitle = data.BookTitle;
                        obj.Description = data.Description;
                        _DatabaseContext.Add(obj);
                        int r = _DatabaseContext.SaveChanges();
                        if (r > 0)
                        {
                            data.returnval = true;
                        }
                        else
                        {
                            data.returnval = false;
                        }
                    }
                }
                else
                {
                    //update Book Details Based on Book Id

                    var update = _DatabaseContext.Books.Where(t => t.BookId == data.BookId).SingleOrDefault();

                    update.BookTitle = data.BookTitle;
                    update.Description = data.Description;

                    _DatabaseContext.Update(update);
                    int r = _DatabaseContext.SaveChanges();

                    if (r > 0)
                    {
                        data.returnval = true;
                    }
                    else {
                        data.returnval = false;
                    }
                }

                data.booklist = _DatabaseContext.Books.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return data;
        }

        [HttpGet]
        [Route("deleterow/{id:int}")]
        public listBookModel deleterow(int id)
        {
            listBookModel obj = new listBookModel();
            try {
                var delete = _DatabaseContext.Books.Where(t => t.BookId == id).SingleOrDefault();

                if (delete!=null)
                {
                    _DatabaseContext.Remove(delete);
                    int r =_DatabaseContext.SaveChanges();
                    if (r > 0)
                    {
                        obj.returnval = true;
                    }
                    else 
                    {
                        obj.returnval = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

      
    }

    public class listBookModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public Array booklist { get; set; }
        public bool returnval { get; set; }
        public bool duplicate { get; set; }

    }
}
