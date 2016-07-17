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
 public class TransactionsController : ODataController
    {
        private FinanceContainer db = new FinanceContainer();

        // GET: odata/Transactions
        [EnableQuery]
        public IQueryable<Transaction> GetTransactions()
        {
            return db.TransactionSet;
        }

        // GET: odata/Transactions(5)
        [EnableQuery]
        public SingleResult<Transaction> GetTransaction([FromODataUri] int key)
        {
            return SingleResult.Create(db.TransactionSet.Where(transaction => transaction.Id == key));
        }

        // PUT: odata/Transactions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Transaction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction transaction = await db.TransactionSet.FindAsync(key);
            if (transaction == null)
            {
                return NotFound();
            }

            patch.Put(transaction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // POST: odata/Transactions
        public async Task<IHttpActionResult> Post(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionSet.Add(transaction);
            await db.SaveChangesAsync();

            return Created(transaction);
        }

        // PATCH: odata/Transactions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Transaction> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction transaction = await db.TransactionSet.FindAsync(key);
            if (transaction == null)
            {
                return NotFound();
            }

            patch.Patch(transaction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // DELETE: odata/Transactions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Transaction transaction = await db.TransactionSet.FindAsync(key);
            if (transaction == null)
            {
                return NotFound();
            }

            db.TransactionSet.Remove(transaction);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Transactions(5)/Plan
        [EnableQuery]
        public IQueryable<Plan> GetPlan([FromODataUri] int key)
        {
            return db.TransactionSet.Where(m => m.Id == key).SelectMany(m => m.Plan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int key)
        {
            return db.TransactionSet.Count(e => e.Id == key) > 0;
        }
    }
}
