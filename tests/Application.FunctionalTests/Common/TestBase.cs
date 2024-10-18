using System;
using AspNetCore.WebApi.Template.Application.Common.Mappings;
using AspNetCore.WebApi.Template.Infrastructure.Data;
using AutoMapper;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public class CommandTestBase : IDisposable
{
    protected readonly ApplicationDbContext _context;
    protected IMapper Mapper { get; private set; }

    public CommandTestBase()
    {
        _context = ApplicationDbContextFactory.Create();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(_context);
    }
}
