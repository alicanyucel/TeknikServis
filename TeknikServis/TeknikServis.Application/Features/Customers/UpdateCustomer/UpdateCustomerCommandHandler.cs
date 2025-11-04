using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IProvinceRepository provinceRepository,
    IDistrictRepository districtRepository,
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("musteri bulunamadi.");
        }

        // Lokasyon alanları gönderilmişse doğrula ve güncelle
        if (request.CountryId.HasValue)
        {
            var countryExists = await countryRepository.GetAll().AnyAsync(c => c.Id == request.CountryId.Value, cancellationToken);
            if (!countryExists)
                return Result<string>.Failure("Geçersiz CountryId. Ülke bulunamadı.");
            customer.CountryId = request.CountryId.Value;
        }
        if (request.ProvinceId.HasValue)
        {
            var province = await provinceRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.ProvinceId.Value, cancellationToken);
            if (province is null)
                return Result<string>.Failure("Geçersiz ProvinceId. İl bulunamadı.");
            if (request.CountryId.HasValue && province.CountryId != request.CountryId.Value)
                return Result<string>.Failure("Province ülke ile uyuşmuyor.");
            customer.ProvinceId = request.ProvinceId.Value;
        }
        if (request.DistrictId.HasValue)
        {
            var district = await districtRepository.GetByExpressionWithTrackingAsync(d => d.Id == request.DistrictId.Value, cancellationToken);
            if (district is null)
                return Result<string>.Failure("Geçersiz DistrictId. İlçe bulunamadı.");
            if (request.ProvinceId.HasValue && district.ProvinceId != request.ProvinceId.Value)
                return Result<string>.Failure("District il ile uyuşmuyor.");
            customer.DistrictId = request.DistrictId.Value;
        }

        mapper.Map(request, customer);
        customerRepository.Update(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Müşteri güncellendi.";

    }
}