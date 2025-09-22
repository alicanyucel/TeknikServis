using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.GetAllPerson;

internal sealed class GetAllPersonQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetAllPersonQuery, Result<List<Person>>>
{
    public async Task<Result<List<Person>>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
    {
        List<Person> person = await personRepository.GetAll().ToListAsync(cancellationToken);
        return person.ToList();
    }
}
