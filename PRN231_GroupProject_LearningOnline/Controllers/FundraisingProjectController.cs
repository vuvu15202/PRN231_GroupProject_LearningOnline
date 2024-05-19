using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Entities.DTO;
using PRN231_GroupProject_LearningOnline.Models;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    [Route("api/projects")]
    [ApiController]
    public class FundraisingProjectController : ControllerBase
    {
        DonationWebApp_v2Context _context = new DonationWebApp_v2Context();

        [Authorize(RoleEnum.Admin)]
        [HttpGet()]
        public IActionResult GetAllProjects()
        {
            var project = _context.FundraisingProjects.ToList();
            return Ok(project);
        }

        [Authorize(RoleEnum.Admin)]
        [HttpPost]
        public IActionResult CreateProject([FromBody] FundraisingProject model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.FundraisingProjects.Add(model);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            else return BadRequest(model);

            return Ok(model);
        }


        [Authorize(RoleEnum.Admin)]
        [HttpPut()]
        public IActionResult EditProject([FromBody] FundraisingProject model)
        {
            
            var checkProject = _context.FundraisingProjects.Find(model.ProjectId);
            if (checkProject != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //checkProject.RoleId = model.RoleId;

                        _context.FundraisingProjects.Update(model);
                        _context.SaveChanges();
                        return Ok(model);
                    }
                    catch
                    {
                        return BadRequest("Fail!");
                    }
                }
                else
                {
                    return BadRequest("This fundraising project is no longer exist!");

                }
            }
            else
            {
                return BadRequest("This fundraising project is no longer exist!");
            }
        }

        

    }
}
