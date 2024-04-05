using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edits
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set;}
        
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _dataContext;
            public IMapper _mapper { get; }
            public Handler(DataContext dataContext, IMapper mapper)
            {
                _mapper = mapper;
                _dataContext = dataContext;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _dataContext.Activities.FindAsync(request.Activity.Id);
                _mapper.Map(request.Activity, activity);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}