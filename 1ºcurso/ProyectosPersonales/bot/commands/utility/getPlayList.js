const {SlashCommandBuilder, MessageFlags} = require('discord.js');
const {createAudioResource, createAudioPlayer, getVoiceConnection, AudioPlayerStatus } = require('@discordjs/voice');
const fs = require('fs');

module.exports = {
    data: new SlashCommandBuilder()
		.setName('getplaylist')
		.setDescription('muestra las playlist Existentes'),
	async execute(interaction) {
                const ruta = './Musicabot';
                let mensaje = '';
                const elementos = fs.readdirSync(ruta); 
                for (const nombre of elementos) { 
                    const info = fs.statSync(`${ruta}/${nombre}`); 
                    if (info.isDirectory()) { mensaje = mensaje + nombre + ' '} 
                }

                interaction.reply({content: mensaje, flags: MessageFlags.Ephemeral});
        },
};