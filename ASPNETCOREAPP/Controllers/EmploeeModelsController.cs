using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETCOREAPP.Models;



namespace ASPNETCOREAPP.Controllers
{
    public class EmploeeModelsController : Controller
    {
        private readonly DatabaseContext _context;

        public EmploeeModelsController(DatabaseContext context)
        {
            _context = context;
        }
        

        // GET: EmploeeModels
        public async Task<IActionResult> Index(string SearchText)
        {
            var Emploees = from em in _context.Emploees
                       select em;

            if (!String.IsNullOrEmpty(SearchText))
            {

                Emploees = Emploees.Where(n => n.Name.Contains(SearchText) || n.Surname.Contains(SearchText) || (n.Name + " " + n.Surname).Contains(SearchText)|| (n.Surname + " " + n.Name).Contains(SearchText));
                return View(await Emploees.ToListAsync());
            }
            else {
                
                return View(await _context.Emploees.ToListAsync());

            }
        }

        // GET: EmploeeModels/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var emploeeModel = await _context.Emploees
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (emploeeModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(emploeeModel);
        //}

        // GET: EmploeeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploeeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Empid,Name,Surname,Gender,DateofBirth,Email,Posti,Statusi,DateofFire")] EmploeeModel emploeeModel)
        {
            
            if (ModelState.IsValid)
            {

                
                bool checkmail = (from email in _context.Emploees
                                  where email.Email == emploeeModel.Email
                                  select email).Any();
                

                bool checkid = (from id in _context.Emploees
                                where id.Empid == emploeeModel.Empid
                                select id).Any();
                if (checkmail)
                {
                    
                    ViewBag.Message = "Aseti Meilit Registrirebuli tanamshromeli ukve arsebobs";
                    return View(emploeeModel);
                }
                
                if (checkid)
                {
                    ViewBag.Message = "Aseti tanamshromeli ukve arsebobs";
                    return View(emploeeModel);
                }



                ViewBag.Message = "tanamshromeli Warmatebit Damaemata";
                _context.Add(emploeeModel);
                
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));

                
            }
            
            return View(emploeeModel);
            
        }

        // GET: EmploeeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emploeeModel = await _context.Emploees.FindAsync(id);
            if (emploeeModel == null)
            {
                return NotFound();
            }
            return View(emploeeModel);
        }

        // POST: EmploeeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Empid,Name,Surname,Gender,DateofBirth,Email,Posti,Statusi,DateofFire")] EmploeeModel emploeeModel)
        {
            if (id != emploeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool checkmail = (from email in _context.Emploees
                                      where email.Email == emploeeModel.Email
                                      select email).Any();


                    bool checkid = (from Id in _context.Emploees
                                    where Id.Empid == emploeeModel.Empid
                                    select Id).Any();
                    if (checkmail)
                    {

                        ViewBag.Message = "Aseti Meilit Registrirebuli tanamshromeli ukve arsebobs";
                        return View(emploeeModel);
                    }

                    if (checkid)
                    {
                        ViewBag.Message = "Aseti tanamshromeli ukve arsebobs";
                        return View(emploeeModel);
                    }


                    _context.Update(emploeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploeeModelExists(emploeeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emploeeModel);
        }

        // GET: EmploeeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emploeeModel = await _context.Emploees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emploeeModel == null)
            {
                return NotFound();
            }

            return View(emploeeModel);
        }

        // POST: EmploeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emploeeModel = await _context.Emploees.FindAsync(id);
            _context.Emploees.Remove(emploeeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploeeModelExists(int id)
        {
            return _context.Emploees.Any(e => e.Id == id);
        }
    }
}
