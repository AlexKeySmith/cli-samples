void Main()
{

	var container = ContainerConfig.Configure();

	using (var scope = container.BeginLifetimeScope())
	{
		var app = scope.Resolve<IApplication>();
		
		app.Run();
	}
}

public static class ContainerConfig
{
	public static IContainer Configure()
	{
		var builder = new ContainerBuilder();
		
		builder.Register(c => new Insulter());
		builder.RegisterType<Application>().As<IApplication>();
		
		builder.RegisterType<MessageService>().As<IMessageService>()
			.EnableInterfaceInterceptors(new ProxyGenerationOptions(new FriendlinessHook<IMessageFriendliness>()));			

		return builder.Build();
	}

}

public class FriendlinessHook<T> : IProxyGenerationHook
{
	public void MethodsInspected()
	{ }

	public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
	{ }

	public Boolean ShouldInterceptMethod(Type type, MethodInfo methodInfo)
	{
		var shouldIntercept = methodInfo.ReturnType.IsAssignableTo<T>();
		
		return shouldIntercept;
	}
}


public class Application : IApplication
{
	private readonly IMessageService messageService;

	public Application(IMessageService messageService)
	{
		this.messageService = messageService;
	}

	public void Run()
	{
		this.messageService.GetMessage().Dump();
		this.messageService.GetColor().Dump();
	}
}


public interface IApplication
{
	void Run();
}


[Intercept(typeof(Insulter))]
public interface IMessageService
{
	//this should be intercepter
	Message GetMessage();
	//don't want this intercepted
	string GetColor();
}


public class MessageService : IMessageService
{
	public Message GetMessage()
	{
		return new Message { Text = "Hello!", FriendlinessFactor = Friendliness.Happy };
	}

	public string GetColor()
	{
		return "Red";
	}
}

public class Message : IMessageFriendliness
{
	public string Text { get; set; }
	public Friendliness FriendlinessFactor {get; set;}
}

public interface IMessageFriendliness
{
	string Text { get; set; }
	Friendliness FriendlinessFactor {get; set;}
}

public enum Friendliness
{
	Happy = 0,
	Insult = 1
}

public class Insulter : IInterceptor
{
	public void Intercept(IInvocation invocation)
	{
		$"begin insult addition...".Dump();
		
		invocation.Proceed();
		
		var friendliness = invocation.ReturnValue as IMessageFriendliness;

		friendliness.Text = $"{friendliness.Text} you poo poo head!";
		friendliness.FriendlinessFactor = Friendliness.Insult;
	}
}