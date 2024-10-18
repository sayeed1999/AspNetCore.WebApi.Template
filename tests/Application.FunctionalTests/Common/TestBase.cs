using System;
using AspNetCore.WebApi.Template.Application.Common.Mappings;
using AspNetCore.WebApi.Template.Infrastructure.Data;
using AutoMapper;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public class TestBase : IDisposable
{
    protected readonly ApplicationDbContext _context;
    protected IMapper _mapper { get; private set; }

    public TestBase()
    {
        _context = ApplicationDbContextFactory.Create();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(_context);
    }
}
