using System.IO;

/// <summary>
/// Redirects the poutput of the console to a textwriter.
/// This way we have the output available in the applicaton
/// </summary>
public class ConsoleRedirect : StringWriter
{
	TextWriter PreviousOutput = null;
	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleRedirect"/> class.
	/// </summary>
	/// <param name="current">The current.</param>
	public ConsoleRedirect(TextWriter current)
	{
		PreviousOutput = current;
	}

	/// <summary>
	/// Writes the specified region of a character array to this instance of the StringWriter.
	/// </summary>
	/// <param name="buffer">The character array to read data from.</param>
	/// <param name="index">The index at which to begin reading from <paramref name="buffer"/>.</param>
	/// <param name="count">The maximum number of characters to write.</param>
	/// <exception cref="T:System.ArgumentNullException">
	/// 	<paramref name="buffer"/> is null.
	/// </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	/// 	<paramref name="index"/> or <paramref name="count"/> is negative.
	/// </exception>
	/// <exception cref="T:System.ArgumentException">
	/// (<paramref name="index"/> + <paramref name="count"/>)&gt; <paramref name="buffer"/>. Length.
	/// </exception>
	/// <exception cref="T:System.ObjectDisposedException">
	/// The writer is closed.
	/// </exception>
	public override void Write(char[] buffer, int index, int count)
	{
		base.Write(buffer, index, count);
		PreviousOutput.Write(buffer, index, count);
	}
}
