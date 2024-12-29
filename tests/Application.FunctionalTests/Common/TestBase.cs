using System;
using Application.Common.Mappings;
using Infrastructure.Data;
using AutoMapper;

namespace Application.FunctionalTests.Common;

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
