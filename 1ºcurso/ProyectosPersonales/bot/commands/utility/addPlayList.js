const {SlashCommandBuilder, MessageFlags} = require('discord.js');
const {createAudioResource, createAudioPlayer, getVoiceConnection, AudioPlayerStatus } = require('@discordjs/voice');
const fs = require('fs');

module.exports = {
    data: new SlashCommandBuilder()
		.setName('addplaylist')
		.setDescription('Añade Una playlist al bot')
        .addStringOption(option => option
            .setName('name')
            .setDescription('Nombre de la playList')
            .setRequired(true)
        ),
	async execute(interaction) {
                const NAME = interaction.options.getString('name');
                console.log(NAME);
                try{
                    fs.mkdirSync(`./Musicabot/${NAME}`);
                    interaction.reply({content: 'Se añadio correctamente',  flags: MessageFlags.Ephemeral});
                }catch(err){
                    if(err.code == "EEXIST") {interaction.reply({content: 'Error: Esta playlist ya fue creada',  flags: MessageFlags.Ephemeral}); }
                    else throw err;
                }
        },
};
