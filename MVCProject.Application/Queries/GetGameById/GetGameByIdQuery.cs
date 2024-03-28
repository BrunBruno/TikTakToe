using MediatR;
using MVCProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Application.Queries.GetGameById {
    public class GetGameByIdQuery : IRequest<Game> { 
        public string Id { get; set; }
        public GetGameByIdQuery(string id) { 
            Id = id;
        }
    }
}
