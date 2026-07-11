const {SlashCommandBuilder, PermissionFlagsBits} = require('discord.js')

module.exports = {
    data: new SlashCommandBuilder()
        .setName('clear')
        .setDescription('Eliminar mensajes')
        .addUserOption(option => option
            .setName('usuario')
            .setDescription('usuario a eliminar mensajes')
            .setRequired(true))
        .setDefaultMemberPermissions(PermissionFlagsBits.Administrator)
        .setDMPermission(false),
    async execute(interaction){
        const user = interaction.options.getUser('usuario')

        const mensaje = await interaction.channel.messages.fetch()
        let mensajeliminar = [];
        let i = 0;
        await mensaje.filter((message)=>{
            if(message.author.id === user.id && 10000 > i){
                mensajeliminar.push(message)
                i++
            }
        });
        interaction.channel.bulkDelete(mensajeliminar, true).then(() => {interaction.reply({content:'Eliminados', ephemeral:true})})
    },
}