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
  public class PlansController : ODataController
    {
        private FinanceContainer db = new FinanceContainer();

        // GET: odata/Plans
        [EnableQuery]
        public IQueryable<Plan> GetPlans()
        {
            return db.PlanSet;
        }

        // GET: odata/Plans(5)
        [EnableQuery]
        public SingleResult<Plan> GetPlan([FromODataUri] int key)
        {
            return SingleResult.Create(db.PlanSet.Where(plan => plan.Id == key));
        }

        // PUT: odata/Plans(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Plan> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Plan plan = await db.PlanSet.FindAsync(key);
            if (plan == null)
            {
                return NotFound();
            }

            patch.Put(plan);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(plan);
        }

        // POST: odata/Plans
        public async Task<IHttpActionResult> Post(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlanSet.Add(plan);
            await db.SaveChangesAsync();

            return Created(plan);
        }

        // PATCH: odata/Plans(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Plan> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Plan plan = await db.PlanSet.FindAsync(key);
            if (plan == null)
            {
                return NotFound();
            }

            patch.Patch(plan);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(plan);
        }

        // DELETE: odata/Plans(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Plan plan = await db.PlanSet.FindAsync(key);
            if (plan == null)
            {
                return NotFound();
            }

            db.PlanSet.Remove(plan);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Plans(5)/Transaction
        [EnableQuery]
        public IQueryable<Transaction> GetTransaction([FromODataUri] int key)
        {
            return db.PlanSet.Where(m => m.Id == key).SelectMany(m => m.Transaction);
        }

        // GET: odata/Plans(5)/TargetCategory
        [EnableQuery]
        public IQueryable<TargetCategory> GetTargetCategory([FromODataUri] int key)
        {
            return db.PlanSet.Where(m => m.Id == key).SelectMany(m => m.TargetCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanExists(int key)
        {
            return db.PlanSet.Count(e => e.Id == key) > 0;
        }
    }
}
