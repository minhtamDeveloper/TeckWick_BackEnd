using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using PlantNestBackEnd.Services.Impl;

namespace PlantNestBackEnd.Controllers;
[Route("api/contact")]
public class ContactController : Controller
{
    private iContact iContact;
    public ContactController(iContact _iContact)
    {
        iContact = _iContact;
    }
    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult findAll()
    {
        try
        {
            var rs = iContact.findAll();
            return Ok(rs);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpPost("created")]
    // cùng lúc nhận hình ảnh và chuỗi JSON
    public IActionResult Create([FromBody] Contact contact)
    {
        try
        {
            bool result = iContact.create(contact);
            return Ok(new
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            // mã 400: có lỗi xảy ra
            return BadRequest(ex);
        }
    }

    //[Produces("application/json")]
    //[HttpGet("contactdetail/{id}")]
    //public IActionResult detail(int id)
    //{
    //    try
    //    {
    //        return Ok(new
    //        {
    //            Result = iContact.findByID(id)
    //        });
    //    }
    //    catch
    //    {
    //        return BadRequest();
    //    }
    //}

    // delete contact
    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(new
            {
                Result = iContact.Delete(id)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
