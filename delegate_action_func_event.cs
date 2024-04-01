
C# 中的delegate、event、Action、Func
都属于委托，只是展现的形式不同而已，无论哪种，其实都可以采用delegate实现，为什么会出现另外的三种呢？

因为delegate是很宽泛的，格式内容都不受限，俗话说没有规矩不成方圆，如果一人过于随意，那么他所做的事也规范不到哪去，这就会导致后期的维护很费劲，实际开发中也基本都用后面三种。
————————————————
版权声明：本文为CSDN博主「悠然少年心」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/qq_44116353/article/details/123414884


C# action,delegate,func的用法和区别
模拟一下场景：小明最近学习情绪高涨，以前买的书已经满足不了欲望，打算去买本（一个程序员的自我修养）。可是呢以前总是跑书厂买，nm，太远了扛不住，就去跑去附近书店去买，小明去给钱就弄了一本书回来，这个过程就是委托。开始分析

1：小明要买一本一个程序员自我修养的书籍（xx书就不买）硬性要求 （这就是要定义委托性质）

代码：

 private delegate void BuyBook();

2：附近书店 （委托的方法）

代码：

public static void Book()
{

    Console.WriteLine("我是提供书籍的");
}
3：小明和书店建立关系（给委托绑定方法） 

代码： 

 BuyBook buybook = new BuyBook(Book);
4：小明给钱拿书（触发）

buybook();
上面的内容是为了能理解委托的用法下面呢我开始讲解Action和Func

Action的用法
1：小明很是苦恼，我就是买一本书籍，每次都让我定义下，烦死了，有没有一种方法不去定义委托呢，那么有吗，还真有，就是我们今天讲的Action

Action BookAction = new Action(Book);
BookAction();

这样是不是就简单了很多

2：小明现在又不满意了，我把一个程序员的自我修养看完了，现在呢想买本其他书，那怎么办，我是不是要重新再次定义委托。其实不需要你只需要把参数穿过来就可以了。下面我们看Action<T> 的用法

 static void Main(string[] args)

{
    Action<string> BookAction = new Action<string>(Book);
    BookAction("百年孤独");

}


public static void Book(string BookName)

{
    Console.WriteLine("我是买书的是:{0}", BookName);

}

3：现在小明又改变主意了，我不仅要自己选择书籍，我还要在一个牛逼的书籍厂家买，有没有这种方式呢，那么告诉你有，Action <in T1,in T2 >

static void Main(string[] args)

{
    Action<string, string> BookAction = new Action<string, string>(Book);
    BookAction("百年孤独", "北京大书店");

}


public static void Book(string BookName, string ChangJia)

{
    Console.WriteLine("我是买书的是:{0}来自{1}", BookName, ChangJia);

}

Func的用法
小明又发生疑问了，每次我自己都去书店去拿书，有没有一种方法直接送到我家里呢，那么Func专门提供了这样的服务

Func 解释 封装一个不定具有参数（也许没有）但却返回 TResult 参数指定的类型值的方法。

1：我们先看一个没有参数只有返回值的方法

 static void Main(string[] args)

{
    Func<string> RetBook = new Func<string>(FuncBook);

    Console.WriteLine(RetBook);

}


public static string FuncBook()

{
    return "送书来了";

}

2：有参数有返回值的方法

static void Main(string[] args)
{
    Func<string, string> RetBook = new Func<string, string>(FuncBook);

    Console.WriteLine(RetBook("aaa"));
}

public static string FuncBook(string BookName)
{
    return BookName;
}

3：Func一个很重要的用处就是传递值，下面我举一个简单的代码来说明

 Func<string> funcValue = delegate

 {
     return "我是即将传递的值3";

 };


DisPlayValue(funcValue);
注释1: DisplayVaue是处理传来的值，比喻缓存的处理，或者统一添加数据库等

 private static void DisPlayValue(Func<string> func)
{
    string RetFunc = func();
    Console.WriteLine("我在测试一下传过来值：{0}", RetFunc);

}

总结
1：Action用于没有返回值的方法（参数可以根据自己情况进行传递）

2：Func恰恰相反用于有返回值的方法（同样参数根据自己情况情况）

3：记住无返回就用action，有返回就用Func



原文链接：https://mp.weixin.qq.com/s/nQwlzw1tyYB4In4Uh1cFLg