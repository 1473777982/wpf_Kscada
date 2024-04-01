using System;

public class Class1
{
  
    myFunction_once()
    {
        //“Task” 只运行一次  延时运行
        Task.Delay(TimeSpan.FromMilliseconds(2000))
             .ContinueWith(task => MessageBox.Show("fsdfs"));
    }   
}
