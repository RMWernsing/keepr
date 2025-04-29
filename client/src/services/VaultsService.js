import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { Keep } from "@/models/Keep.js"

class VaultsService {
  async getKeepsForVault(vaultId) {
    AppState.keeps = []
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    // logger.log('here are your keeps for this vault', response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps
  }
  async deleteVault(vaultId) {
    const response = await api.delete(`api/vaults/${vaultId}`)
    logger.log('You deleted your Vault!', response.data)
  }
  async privateVault(vaultStatus, vaultId) {
    const response = await api.put(`api/vaults/${vaultId}`, { isPrivate: vaultStatus })
    // logger.log('here is your edited vault', response.data)
    const vault = new Vault(response.data)
    AppState.activeVault = vault
  }
  async getVaultById(vaultId) {
    AppState.activeVault = null
    const response = await api.get(`api/vaults/${vaultId}`)
    // logger.log('here is your active vaults', response.data)
    const vault = new Vault(response.data)
    AppState.activeVault = vault
  }
  async createVault(vaultData) {
    const response = await api.post('api/vaults', vaultData)
    // logger.log('here is your vault', response.data)
    const vault = new Vault(response.data)
    AppState.myVaults.push(vault)
    if (AppState.activeProfile === null || vault.creatorId === AppState.activeProfile.id) {
      AppState.vaults.push(vault)
    }
  }

}

export const vaultsService = new VaultsService()