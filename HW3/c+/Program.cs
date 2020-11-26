using System; 
using System.Drawing;
using System.Drawing.Imaging;
using System.IO; 
using System.Threading; 
  
public class ErrorDiffusion{ 

	public static int N = 1; 
	public static int Height, Weight;
    public static int[,] Input;
	public static int[,] Output;
	public static bool[,] Done;
	public static void ProcessRow(object x) {
		
		int h = (int)x;
		
		while (h <= Height) {
			
            for (int w = 1; w <= Weight; w++) {
				
				while (!Done[h-1,w+1]) {
					// wait until the upper row finish error diffusion (spin lock) 
				}

                Output[h,w] = (Input[h,w] < 128 ? 0 : 1);

                int err = Input[h,w] - (255 * Output[h,w]);

                Input[h,w+1] += (err * 7) / 16;
                Input[h+1,w-1] += (err * 3) / 16;
                Input[h+1,w] += (err * 5) / 16;
                Input[h+1,w+1] += (err * 1) / 16;
                Input[h+1,w+1] += (err * 1) / 16;
				
				Done[h,w] = true;
            }
			
			h = h + N;
        }
	}
  
	public static void Main() {
		
		Bitmap bmp = new Bitmap(@"C:\Users\Fame\Desktop\ProjectPrograming'\OS\HW\HW4\c+\test.bmp");
		Height = bmp.Height;
		Weight = bmp.Width;

		Input = new int[Height+2,Weight+2];	    // default value = 0
		Output = new int[Height+2,Weight+2];	// default value = 0
		Done = new bool[Height+2,Weight+2];	    // default value = false

        for (int w = 0; w < Weight+2; w++) { 	// initialize the first and the last rows
            Done[0,w] = true;                   // make sure that the first index will not be the problem for algorithm
            Done[Height+1,w] = true;
        }

        for (int h = 0; h < Height+2; h++) {    // initialize the first and the last columns
            Done[h,0] = true;
            Done[h,Weight+1] = true;
        }

		for (int h = 1; h <= Height; h++) {     // read an image file and keep in input array
			for (int w = 1; w <= Weight; w++) {
				Color px = bmp.GetPixel(w-1, h-1);
				Input[h,w] = px.R;
			}
		}
		
		Thread[] t = new Thread[N];		        // create threads
		for (int i = 0; i < N; i++) {
			t[i] = new Thread(ProcessRow); 
		}
		long time1 = DateTime.Now.Ticks;        // get start timer
		
		for (int i = 0; i < N; i++) {           // start threads, so all the threads will execute
			t[i].Start(i+1);
		}
		
		for (int i = 0; i < N; i++) {
			t[i].Join();                        // protect this program to execute in main before all threads is finished executed 
		}

		long time2 = DateTime.Now.Ticks;        // get end timer
		
		Console.WriteLine("Execution time (seconds):");       // display execution time
		Console.WriteLine((time2-time1)*(Math.Pow(10,-7)));

		for (int h = 1; h <= Height; h++) {
            for (int w = 1; w <= Weight; w++) {
				if 	(Output[h,w] == 0) {
                    bmp.SetPixel(w-1, h-1, Color.Black);
                }    
				else if (Output[h,w] == 1){
                    bmp.SetPixel(w-1, h-1, Color.White);
                } 
				else {
					throw new Exception("Error: a pixel must be either black or white");
				}
			}
		}
		
		bmp.Save("output.png", ImageFormat.Png);
    } 
} 
