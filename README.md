# Putty Known Hosts

You can download a binary at http://codebad.com/u/putty-known-hosts.exe

This is a really simple GUI tool for reviewing, removing, or exporting host
key verifications stored in the Windows Registry by the venerable PuTTY SSH
[1] client by Simon Tatham.

You can also un-hash hostnames interactively by typing a matching host into the
search box. This can be useful as an intermediate step in importing
OpenSSH-format `known_hosts` entries into PuTTY because PuTTY's known hosts
records don't appear to support hashing (or at least, it isn't the default
option.)

It has extremely limited support for keys (2048-bit RSA keys are the only
ones that have been tested.)

Export function exports to Windows' "Registration Entries" (*.reg) file
format, which can be imported to other Windows users' registries with a
double click. (You probably want to verify such a file first!)

I don't actually know C#. Critiques and PRs very welcome!

[1] https://www.chiark.greenend.org.uk/~sgtatham/putty/
