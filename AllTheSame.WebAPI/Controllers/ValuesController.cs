using System.Collections.Generic;
using System.Web.Http;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    /// </summary>
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        ///     Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        ///     Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}