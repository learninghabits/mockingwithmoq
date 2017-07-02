using mocking_with_moq.Repositories;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mocking_with_moq.Controllers
{
    public class TopicController : ApiController
    {
        IRepository<Topic> _repository;
        public TopicController(IRepository<Topic> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        public TopicController() : this(new Repository<Topic>())
        { }

        // GET api/topics
        [Route("api/topics")]
        public HttpResponseMessage Get()
        {
            var topics = _repository.All
                          .ToList()
                          .Select(t =>
                          {
                              dynamic expando = new ExpandoObject();
                              expando.id = t.Id;
                              expando.name = t.Name;
                              return expando;
                          })
                          .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, topics);
        }

        // GET api/topic/2
        public HttpResponseMessage Get(int id)
        {
            var topic = _repository.Find(id);
            if (topic == null)
            {
                dynamic expando = new ExpandoObject();
                expando.message = "The topic you requested was not found";
                return Request.CreateResponse(HttpStatusCode.NotFound, expando as object);
            }

            dynamic data = new ExpandoObject();
            data.id = topic.Id;
            data.name = topic.Name;
            data.tutorials = topic.Tutorials
                                  .ToList()
                                  .Select(t =>
                                  {
                                      dynamic expando = new ExpandoObject();
                                      expando.id = t.Id;
                                      expando.name = t.Name;
                                      expando.website = t.WebSite;
                                      expando.type = t.Type;
                                      expando.url = t.Url;
                                      return expando;
                                  })
                                  .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, data as object);
        }

        [Route("api/topic/{id}/{name}")]
        // GET api/topic/2
        public HttpResponseMessage Get(int id, string name)
        {
            var tutorials = _repository.Where(t => t.Id == id)
                                 .SelectMany(t => t.Tutorials)
                                 .Where(t => t.Name == name)
                                 .ToList();

            if (tutorials.Count == 0)
            {
                dynamic expando = new ExpandoObject();
                expando.message = "The tutorial  you requested was not found";
                return Request.CreateResponse(HttpStatusCode.NotFound, expando as object);
            }

            dynamic data = new ExpandoObject();
            data.id = id;
            data.name = name;
            data.tutorials = tutorials
                             .ToList()
                             .Select(t =>
                             {
                                 dynamic expando = new ExpandoObject();
                                 expando.id = t.Id;
                                 expando.name = t.Name;
                                 expando.website = t.WebSite;
                                 expando.type = t.Type;
                                 expando.url = t.Url;
                                 return expando;
                             })
                             .ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data as object);
        }

        // POST api/values
        public HttpResponseMessage Post(Topic topic)
        {
            _repository.Insert(topic);
            _repository.Save();
            dynamic expando = new ExpandoObject();
            expando.id = topic.Id;
            expando.url = Request.RequestUri.AbsoluteUri + "/" + topic.Id;
            return Request.CreateResponse(HttpStatusCode.OK, expando as object);
        }

        public HttpResponseMessage Put(Topic topic)
        {
            _repository.Update(topic);
            _repository.Save();
            dynamic expando = new ExpandoObject();
            expando.message = "topic is updated successfully";
            return Request.CreateResponse(HttpStatusCode.OK, expando as object);
        }

        // DELETE api/topic/2
        public HttpResponseMessage Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            dynamic expandoO = new ExpandoObject();
            expandoO.message = "The topic was deleted successfully";
            return Request.CreateResponse(HttpStatusCode.OK, expandoO as object);
        }
    }
}
