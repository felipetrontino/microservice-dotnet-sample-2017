dotnet ef --startup-project ../tools/ migrations --verbose --prefix-output add --context DbMigrations $("Book_$([int64](([datetime]::UtcNow)-(get-date "1/1/1970")).TotalSeconds)")
