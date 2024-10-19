# Application Layer

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project.

Any command or query request that **Mediatr** sends to this layer, atfirst passes through the **Fluent Validation** middleware pipeline, once validated reaches the handlers defined in this layer.

This layer defines interfaces that are implemented by outside layers.

For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.
