#### 1.1.2 (2022-06-23)

- Package updates.

#### 1.1.1 (2022-03-29)

- Code cleanup.

#### 1.1.0 (2021-10-15)

Complete overhaul of the internal implementation.

- **Breaking Changes** - All methods now require a cancellation token to the passed in.
- **Revised** - The internal implementation of `NhtsaVpicClient` to be fully asynchronous and cancellable.
- **Added** - An `INhtsaVpicClient` interface to use for dependency injection.
- **Revised** - All request and response objects to be better structured.
- **Added** - A new `ResponseStatus` enum that better indicates the status of a response.
- **Added** - A new `Arex388.NhtsaVpic.Extensions.Microsoft.DependencyInjection` NuGet package to quickly and easily add the client to `IServiceCollection` for dependency injection.
- **Added** - Basic unit tests so I don't have to use LINQPad for testing anymore.

#### 1.0.4 (2021-03-21)

- Minor internal changes.

#### 1.0.3 (2020-05-07)

- Internal cleanup and hopefully performance optimizations by adding `ConfigureAwait(false)` to all `await` calls.