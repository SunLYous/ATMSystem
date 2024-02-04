using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace ATMSystem.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type operation_type as enum
        (
            'top_account' ,
            'withdraw_money'
        );
        
        create table accounts
        (
            account_id bigint primary key generated always as identity ,
            account_pin bigint not null ,
            money bigint not null
        );

        create table admins
        (
            admin_id bigint primary key generated always as identity ,
            admin_password  bigint not null
        );
        
        create table history
        (
            account_id bigint not null references accounts(account_id) ,
            data TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
            operation operation_type not null ,
            value bigint not null ,
            balance bigint not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table accounts;
        drop table admins;
        drop table history; 
        drop type operation_type;
        """;
}