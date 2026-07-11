const {SlashCommandBuilder} = require('discord.js');
const {createAudioResource, createAudioPlayer, getVoiceConnection, AudioPlayerStatus } = require('@discordjs/voice');
const fs = require('fs')

module.exports = {
    data: new SlashCommandBuilder()
        .setName('addmusic')
        .setDescription('Añade musica al bot')
        .addAttachmentOption(option => option
                .setName('archivo')
                .setDescription('musica que descargar')
                .setRequired(true)),
    async execute(interaction){
        interaction.reply({content:'Reproduciendo...',ephemeral:true});
        const conection = getVoiceConnection(interaction.guild.id)
        const file = interaction.options.getAttachment('archivo')
        
    }
};