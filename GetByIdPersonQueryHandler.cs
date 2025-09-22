using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

using TeknikServis.Application.Features.Persons.GetByIdPerson; 

public sealed class GetPersonByIdQueryHandler(IPersonRepository personRepository, IMapper mapper) : IRequestHandler<GetPersonByIdQuery, Result<Person>> 
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Person>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var personEntity = await _personRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (personEntity is null)
            return Result<Person>.Failure("Personel bulunamadý.");

        var person = _mapper.Map<Person>(personEntity);
        return Result<Person>.Succeed(person);
    }
}
