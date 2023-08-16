# Mounts

Mounts are a powerful type dictionary that is designed to allow for a wide array
of software to store and read their own opaque states without colliding with
other software in the same environment.

Mounts are present throughout many stages in the lifecycle of a Lilikoi container:
 - Creation/Compilation
 - Injecting Variables
 - Wrapping Entry Points
 - Injecting Parameters and Wildcards

Mounts can be used to store configuration, factories, and even other Lilikoi containers. 
The only requirement is that each entry into the mount have a unique type.

Additionally, several Lilikoi Types expose a mount interface. You can add entries to a `LilikoiMutator`'s
mount and see those entries in the finalized `LilikoiContainer`'s mount.

For example, a target attribute can place metadata (or even itself!) into the LilikoiMutator
for the creator of the Container to consume for metadata about the target. This functionality
is used frequently in [BitMod](https://github.com/Mooshua/BitMod)

## Performance

Mounts are slower than normal dictionaries, but not by much.
A mount typically takes 30-50ns to look up a single object, regardless of
how many objects are in the mount. 

Writing and checking for existance is typically
in the 20ns range as it does not need to unbox anything.

Performance for mounts is not really a big deal, as they are usually read only once
or twice by a consuming program, compared to normal dictionaries which are constantly
enumerated and updated.
