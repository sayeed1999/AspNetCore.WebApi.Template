# Presentation Layer

This layer contains classes for accepting http requests and returning http responses only.

This layers performs no responsibility except sending commands or queries to **Application Layer** with the help of **Mediatr** and returning responses thereby.

Any application specific logic is strictly prohibited to implement in this layer.
