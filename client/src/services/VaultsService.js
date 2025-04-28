import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService {
  async privateVault(vaultStatus, vaultId) {
    const response = await api.put(`api/vaults/${vaultId}`, { isPrivate: vaultStatus })
    logger.log('here is your edited vault', response.data)
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
    if (AppState.activeProfile === null || vault.creatorId === AppState.activeProfile.id) {
      AppState.vaults.push(vault)
    }
  }

}

export const vaultsService = new VaultsService()