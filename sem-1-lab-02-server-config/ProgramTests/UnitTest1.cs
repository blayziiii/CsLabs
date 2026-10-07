using NUnit.Framework;
using MyProgram = sem_1_lab_02_server_config.Program;

public class ProgramTests
{
    [Test]
    public void CheckConfiguration_GoodConfiguration_ServerCanStart()
    {
        var result = MyProgram.CheckConfiguration(8, 20, 10, true, false);

        Assert.That(result, Is.EqualTo("Запуск сервера разрешен!"));
    }

    [Test]
    public void CheckConfiguration_NotEnoughRam_ServerCannotStart()
    {
        var result = MyProgram.CheckConfiguration(3, 20, 10, true, false);

        Assert.That(result, Is.EqualTo("Запуск сервера невозможен!"));
    }

    [Test]
    public void CheckConfiguration_LowRam_ServerStartsWithWarning()
    {
        var result = MyProgram.CheckConfiguration(6, 20, 10, true, false);

        Assert.That(
            result,
            Is.EqualTo("Запуск сервера возможен, но необходимо предупредить администратора."));
    }

    [Test]
    public void CheckConfiguration_TooManyPlayers_ServerCannotStart()
    {
        var result = MyProgram.CheckConfiguration(8, 51, 10, true, false);

        Assert.That(result, Is.EqualTo("Запуск сервера невозможен!"));
    }

    [Test]
    public void CheckConfiguration_ManyPlayers_ServerStartsWithWarning()
    {
        var result = MyProgram.CheckConfiguration(8, 40, 10, true, false);

        Assert.That(
            result,
            Is.EqualTo("Запуск сервера возможен, но необходимо предупредить администратора."));
    }

    [Test]
    public void CheckConfiguration_TooManyMods_ServerCannotStart()
    {
        var result = MyProgram.CheckConfiguration(8, 20, 51, true, false);

        Assert.That(result, Is.EqualTo("Запуск сервера невозможен!"));
    }

    [Test]
    public void CheckConfiguration_ManyMods_ServerStartsWithWarning()
    {
        var result = MyProgram.CheckConfiguration(8, 20, 40, true, false);

        Assert.That(
            result,
            Is.EqualTo("Запуск сервера возможен, но необходимо предупредить администратора."));
    }

    [Test]
    public void CheckConfiguration_NoBackup_ServerStartsWithWarning()
    {
        var result = MyProgram.CheckConfiguration(8, 20, 10, false, false);

        Assert.That(
            result,
            Is.EqualTo("Запуск сервера возможен, но необходимо предупредить администратора."));
    }

    [Test]
    public void CheckConfiguration_MaintenanceMode_ServerCannotStart()
    {
        var result = MyProgram.CheckConfiguration(8, 20, 10, true, true);

        Assert.That(result, Is.EqualTo("Запуск сервера невозможен!"));
    }
}