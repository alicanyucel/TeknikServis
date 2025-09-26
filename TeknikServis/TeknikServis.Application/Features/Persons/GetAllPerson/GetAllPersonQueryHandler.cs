using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.GetAllPerson;

internal sealed class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, Result<List<Person>>>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Result<List<Person>>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<Person>>.Succeed(persons);
    }
}
