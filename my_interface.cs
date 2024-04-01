//1、C#接口的作用
//C# 接口是一个让很多C#初学者容易迷糊的东西，用起来好像很简单，定义接口，里面包含方法，但没有方法具体实现的代码，然后在继承该接口的类里面要实现接口的所有方法的代码。

//没有真正认识到接口的作用的时候就觉得用接口是多此一举。

//当然你这样想那是绝对绝对错误的，比尔盖茨的微软请的员工都是比盖茨还聪明的人，他们的C#能添这样的多足吗？！关于接口的作用，网上有一位就真的深入浅出给我们做了很好理解的分析。

//我们定义一个接口：

//public interface IBark
//{
//    void Bark();
//}
//再定义一个类,继承于IBark,并且必需实现其中的Bark()方法

//public class Dog : IBark
//{
//    public Dog()
//    { }
//    public void Bark()
//    {
//        Consol.write("汪汪");
//    }
//}
//然后,声明Dog的一个实例,并调用Bark()方法

//Dog 旺财=new Dog();
//旺财.Bark();
//试想一样：
//若是想调用Bark()方法,只需要在Dog()中声明这样的一个方法不就行了吗,干什么还要用接口呢？
//因为接口中并没有Bark()具体实现.真的实现还是要在Dog()中.那么使用接口不是多此一举吗?

//还有人是这样说的:从接口的定义方面来说,接口其实就是类和类之间的一种协定,一种约束.还拿上面的例子来说.所有继承了IBark接口的类中必需实现Bark()方法.那么从用户(使用类的用户)的角度来说,如果他知道了某个类是继承于IBark接口,那么他就可以放心大胆的调用Bark()方法,而不用管Bark()方法具体是如何实现的.比如,我们另外写了一个类。

//public class Cat : IBark
//{
//    public Cat()
//    { }
//    public void Bark()
//    {
//        Consol.write("喵喵");
//    }
//}

//当用户用到Cat类或是Dog类的时候,知道他们继承于IBark,那么不用管类里的具体实现,而就可以直接调用Bark()方法,因为这两个类中肯定有关于Bark()方法的具体实现.

//如果我们从设计的角度来看.一个项目中用若干个类需要去编写,由于这些类比较复杂,工作量比较大,这样每个类就需要占用一个工作人员进行编写.比如A程序员去定Dog类,B程序员去写Cat类.

//这两个类本来没什么联系的,可是由于用户需要他们都实现一个关于”叫”的方法.

//这就要对他们进行一种约束，让他们都继承于IBark接口。

//目的是方便统一管理.另一个是方便调用。

//当然了,不使用接口一样可以达到目的。只不过这样的话,这种约束就不那么明显。

//如果这样类还有Duck类等等,比较多的时候难免有人会漏掉这样方法.所以说还是通过接口更可靠一些,约束力更强一些。

//2、C#中接口的深入浅出:

//通过学习对C#中接口的作用有了更进一步的理解，拿出来跟大家分享一下，有说的不对的地方请大家指教。

//假设我们公司有两种程序员：VB程序员，指的是用VB写程序的程序员，用clsVBProgramer这个类表示；Delphi程序员指的是用Delphi写程序的程序员，用clsDelphiProgramer这个类来表示。每个类都有一个WriteCode()方法。定义如下：

//class clsVBProgramer
//{
//    public void WriteCode()
//    {
//        Console.WriteLine("用VB写程序的程序员");
//    }
//}

//class clsDelphiProgramer
//{
//    public void WriteCode()
//    {
//        Console.WriteLine("用Delphi写程序的程序员");
//    }
//}

//class Program
//{
//    public void WritePrograme(clsVBProgramer programer)//用VB写代码
//    {
//        programer.WriteCode();
//    }
//    public void WritePrograme(clsDelphiProgramer programer)//重载方法，用Delphi写代码
//    {
//        programer.WriteCode();
//    }

//    static void Main(string[] args)
//    {
//        Program proj = new Program();
//        //如果需要用VB写代码
//        clsVBProgramer programer1 = new clsVBProgramer();
//        proj.WritePrograme(programer1);
//        //如果需要用Delphi写代码
//        clsDelphiProgramer programer2 = new clsDelphiProgramer();
//        proj.WritePrograme(programer2);
//    }
//}
//但是如果这时公司又来了一个C#程序员，我们怎么改这段程序，使它能够实现用C#写程序的功能呢？我们需要增加一个新类clsCSharpProgramer,同时在此clsProject这个类中要再次重载WritePrograme（clsCSharpProgramer programer）方法。这下麻烦多了。如果还有C程序员，C++程序员，JAVA程序员呢。麻烦大了！

//但是如果改用接口，就完全不一样了：
//首先声明一个程序员接口：

//interface IProgramer
//{
//    void WriteCode();
//}

//class clsVBProgramer : IProgramer
//{
//    public void WriteCode()
//    {
//        Console.WriteLine("用VB写程序的程序员");
//    }
//}

//class clsDelphiProgramer : IProgramer
//{
//    public void WriteCode()
//    {
//        Console.WriteLine("用Delphi写程序的程序员");
//    }
//}

//class Program
//{
//    public void WritePrograme(IProgramer programer)
//    {
//        programer.WriteCode();
//    }

//    static void Main(string[] args)
//    {
//        Program prog = new Program();
//        IProgramer programer;
//        programer = new clsVBProgramer();
//        prog.WritePrograme(programer);
//        programer = new clsDelphiProgramer();
//        prog.WritePrograme(programer);
//    }
//}
//如果再有C#，C，C++，JAVA这样的程序员添加进来的话，我们只需把它们相关的类加进来，然后在main()中稍做修改就OK了。扩充性特别好！

//另外我们如果把Program这个类封成一个组件，那么当我们的用户需要要扩充功能的时候，我们只需要在外部做很小的修改就能实现，可以说根本就用不着改动我们已经封好组件！是不是很方便，很强大。