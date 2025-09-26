using AutoMapper;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.GetByIdPerson;

public sealed class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Result<Person>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Result<Person>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var personEntity = await _personRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (personEntity is null)
            return Result<Person>.Failure("Personel bulunamadı veya silinmiş.");

        var person = _mapper.Map<Person>(personEntity);
        return Result<Person>.Succeed(person);
    }
}
