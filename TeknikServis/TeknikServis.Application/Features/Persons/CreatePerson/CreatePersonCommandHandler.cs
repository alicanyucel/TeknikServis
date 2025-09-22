using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.CreatePerson;

internal sealed class CreatePersonComamndHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreatePersonCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = mapper.Map<Person>(request);
        await personRepository.AddAsync(person, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Personel kaydı yapıldı";
    }
}