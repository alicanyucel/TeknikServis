using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Statuses.CreateSratus;

internal sealed class CreateStatusComamndHandler(IStatusRepository statusRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
    {
        Status status = mapper.Map<Status>(request);
        await statusRepository.AddAsync(status, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Durum kaydı yapıldı";
    }
}
