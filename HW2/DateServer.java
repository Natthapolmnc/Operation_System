import java.net.*;
import java.io.*;

public class DateServer{

	public static void main(String[] args){
		try {
			ServerSocket sock=new ServerSocket(7066);
			while (true){
				System.out.println("Waiting for client.... at port 7063");
				Socket client= sock.accept();
				System.out.println("client connected");
				PrintThread thread=new PrintThread(client);
				thread.start();
			}
		}
		catch (IOException ioe) {
			System.err.println(ioe);
		}
	}

}
public class PrintThread extends Thread{
	protected Socket s;

	PrintThread(Socket s){
		this.s=s;
	}

	public void run(){
	try{
		PrintWriter pout=new PrintWriter(s.getOutputStream(),true);
		pout.println(new java.util.Date().toString());
		while (true) {
		Thread.sleep(2000);
		pout.println(new java.util.Date().toString());}

	}catch(Exception ioe){System.out.println(ioe);
	}
	}
}
