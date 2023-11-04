var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGenericService<SomeInput, string?>, ConcreteService>();

var app = builder.Build();

app.MapGet("/", (IGenericService<SomeInput, string?> service) 
    => "Maybe? " + service.Get(new SomeInput(Random.Shared.Next())));

app.Run();

public interface IInputConstraint<TOutput>;

public interface IGenericService<TInput, TOutput> where TInput : IInputConstraint<TOutput>
{
    TOutput Get(TInput input);
}

public record SomeInput(int Value) : IInputConstraint<string?>;

public class ConcreteService : IGenericService<SomeInput, string?>
{
    public string? Get(SomeInput input) => input.Value % 2 == 0 ? input.Value.ToString() : null;
}