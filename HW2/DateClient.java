import java.net.*;
import java.io.*;

public class DateClient{

	public static void main(String[] args) {
		try {
			Socket sock=new Socket("127.0.0.1",7066);
			InputStream in=sock.getInputStream();
			BufferedReader bin= new BufferedReader(new InputStreamReader(in));
			String line;
			while ((line = bin.readLine()) != null){
				System.out.println(line);
			}
		}
		catch (IOException ioe) {
			System.err.println(ioe);
		}
	}
}
