## Instruction

1. Download and install .NET Core 2.1 SDK

    - [macOS](https://www.microsoft.com/net/download/macos)
    - [Windows](https://www.microsoft.com/net/download/windows)
    - [Linux](https://www.microsoft.com/net/download/linux)

2. Clone repository and change directory to the project
```
$ git clone https://github.com/echeon/recon.git
$ cd recon
```

3. Run dotnet-cli command to build and run the project
```
$ dotnet run -- [INPUT_PATH] [OUTPUT_PATH]
```
The program will read the text file from `INPUT_PATH` and save the output to `OUTPUT_PATH`. If neither is given, it will use the sample file in `recon.in` and output to `recon.out` in the project directory.