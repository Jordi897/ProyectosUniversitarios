const {createAudioResource, createAudioPlayer, getVoiceConnection, AudioPlayerStatus, joinVoiceChannel } = require('@discordjs/voice');
const { SlashCommandBuilder, VoiceChannel, MessageFlags } = require('discord.js');
const fs = require('fs');

module.exports = {
	data: new SlashCommandBuilder()
		.setName('playdjmusic')
		.setDescription('reproducir'),
	async execute(interaction) {
                const member = interaction.member
                if(member.voice.channel){
                        const conection = joinVoiceChannel({
                                channelId: member.voice.channelId,
                                guildId: interaction.guildId,
                                adapterCreator: interaction.guild.voiceAdapterCreator
                        });
                };
                const conection = getVoiceConnection(interaction.guild.id)
                const player = createAudioPlayer()
                const FileMusic = fs.readdirSync('./Musicabot');
                const textchannel = interaction.channel
                let n = 0;
                let resouce = createAudioResource(`./Musicabot/${FileMusic[n]}`)
                player.play(resouce);
                conection.subscribe(player);
                player.on('error', () =>{
                        conection.destroy()
                });
                player.on(AudioPlayerStatus.Idle,()=>{
                        n++;
                        if (n==(FileMusic.length-1)){
                                n=0;
                        }
                        resouce = createAudioResource(`./Musicabot/${FileMusic[n]}`)
                        player.play(resouce)
                        conection.subscribe(player)
                });
        },
};