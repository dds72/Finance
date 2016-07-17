using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using FinanceService.Models;

namespace FinanceService.Controllers
{
 public class TargetCategoriesController : ODataController
    {
        private FinanceContainer db = new FinanceContainer();

        // GET: odata/TargetCategories
        [EnableQuery]
        public IQueryable<TargetCategory> GetTargetCategories()
        {
            return db.TargetCategorySet;
        }

        // GET: odata/TargetCategories(5)
        [EnableQuery]
        public SingleResult<TargetCategory> GetTargetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.TargetCategorySet.Where(targetCategory => targetCategory.Id == key));
        }

        // PUT: odata/TargetCategories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<TargetCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TargetCategory targetCategory = await db.TargetCategorySet.FindAsync(key);
            if (targetCategory == null)
            {
                return NotFound();
            }

            patch.Put(targetCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(targetCategory);
        }

        // POST: odata/TargetCategories
        public async Task<IHttpActionResult> Post(TargetCategory targetCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TargetCategorySet.Add(targetCategory);
            await db.SaveChangesAsync();

            return Created(targetCategory);
        }

        // PATCH: odata/TargetCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TargetCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TargetCategory targetCategory = await db.TargetCategorySet.FindAsync(key);
            if (targetCategory == null)
            {
                return NotFound();
            }

            patch.Patch(targetCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(targetCategory);
        }

        // DELETE: odata/TargetCategories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            TargetCategory targetCategory = await db.TargetCategorySet.FindAsync(key);
            if (targetCategory == null)
            {
                return NotFound();
            }

            db.TargetCategorySet.Remove(targetCategory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TargetCategories(5)/Plan
        [EnableQuery]
        public SingleResult<Plan> GetPlan([FromODataUri] int key)
        {
            return SingleResult.Create(db.TargetCategorySet.Where(m => m.Id == key).Select(m => m.Plan));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TargetCategoryExists(int key)
        {
            return db.TargetCategorySet.Count(e => e.Id == key) > 0;
        }
    }
}
