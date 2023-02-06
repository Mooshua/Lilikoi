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
