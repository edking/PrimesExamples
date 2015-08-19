Memory Leak Lab
(also...working with IIS worker processes)
------------------------------------------

LAB 1 -- let's see the error
-----
1) Build and deploy the WCFPrimes project locally.  (I used port 888.)
   WCFPrimes can give you a list of primes or tell you if a given number
   is prime.
2) Build and deploy the WebPrimes project locally.  (I used port 889.)
   WebPrimes needs to be in a different app pool than WCFPrimes.
   WebPrimes has 2 submit buttons.  One to check one number.  Another to give
   you a list of things between 2 boundaries.  It is implemented in probably the
   worst way possible -- by checking each number individually.
3) Open the webprimes page and check a few numbers, to be sure it all works.
   Now, as it for a list of primes from 5 to 50.
4) Using perfview, take a GCDump of the WebPrimes worker process.
5) Ask WebPrimes for a list of primes from 5 to 5000.
6) Using perfview, take a GCDump of the WebPrimes worker process.
7) In Perfview, open both of these dumps.
8) With the second one showing, tell it to take a diff, using the first as a baseline.
9) The ServiceChannel<IPrimes> leak should pop right out at you.  (Notice the Exc Cnt 
   and Inc Cnt columns...the number of these things still hanging around.  When I did it,
   there were something like 79K of these still in play.)

ServiceChannel<IPrimes>....sounds a whole lot like making WCF calls to our primes service, doesn't it?
Pop over to Default.aspx.cs in the WebPrimes project.  Pre-WCF this code would have been fine just as it
is.  Now, we need the explicit close.  Uncomment line 39.

To make the error more visible, Webprimes is built in the least efficient manner possible -- it 
uses the "is this a prime?" web service to check each number individually instead of just asking
for a list.

LAB 2 -- see the fix
-----
Repeat the process above, starting at #2 and using the newly modified code.

When I did it, the second dump was actually smaller than the first.  But, either way, look for the
ServiceChannel<IPrimes> object counts and see how many of these are still sticking around.
When I did it, there were only 3 of these still in play.
