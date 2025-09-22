using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.DeletePerson;

public sealed class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result<string>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (person == null)
            return Result<string>.Failure("Personel bulunamadı.");
        _personRepository.Delete(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Personel silindi";
    }
}